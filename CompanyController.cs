using CologneStore.Constants;
using CologneStore.Data;
using CologneStore.DTO;
using CologneStore.ImageService;
using CologneStore.Models;
using CologneStore.Repositories;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CologneStore.Controllers
{
    //[Authorize(Roles = nameof(Roles.Company))]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IFileService _fileService;
        private readonly ApplicationDbContext _context;

        FirestoreDb firestoreDb = FirestoreDb.Create("cologne-store");

        public CompanyController(ICompanyRepository companyRepository, IFileService fileService, ApplicationDbContext context)
        {
            _companyRepository = companyRepository;
            _fileService = fileService;
            _context = context;
        }
        public ActionResult NoCompany()
        {
            return View();
        }
        public async Task<ActionResult> GetCompanies()
        {           
            return View(await _companyRepository.GetCompanies());          
		}


        [HttpGet]
        public ActionResult Create()
        {
            return View(new CompanyDTO());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CompanyDTO companyDTO)
        {
            if (ModelState.IsValid)
                return View(companyDTO);

            try
            {
                if (companyDTO.ImageFile != null)
                {
                    if (companyDTO.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException("Image file cannot exceed 1MB");
                    }
                    string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                    string imageName = await _fileService.SaveFile(companyDTO.ImageFile, allowedExtensions);
					companyDTO.CIPCImage = imageName;
                }

                Company company = new Company()
                {
                    id = companyDTO.id,
                    CompanyName = companyDTO.CompanyName,
                    RegistrationNumber = companyDTO.RegistrationNumber,
                    CompanyAddress = companyDTO.CompanyAddress,
                    CIPCImage = companyDTO.CIPCImage,
                };

                //await _companyRepository.AddCompany(company);
                //TempData["SuccessMessage"] = "Company has been successfully verified!";
                //return RedirectToAction(nameof(GetCompanies));

                bool registrationExists = _context.CompanyRegistrations
                                              .Any(c => c.RegistrationNumber == companyDTO.RegistrationNumber);

                if (registrationExists)
                {
                    await _companyRepository.AddCompany(company);


                    CollectionReference col = firestoreDb.Collection("Company");
                    string id = col.Document().Id;
                    Dictionary<string, object> data = new Dictionary<string, object>()
                    {
                        {"id",id},
                        {"company name", companyDTO.CompanyName},
                        {"registration number", companyDTO.RegistrationNumber},
                        {"cipc cert", companyDTO.CIPCImage},
                    };
                    await col.AddAsync(data);

                    TempData["SuccessMessage"] = "Company has been successfully verified!";
                    return RedirectToAction(nameof(GetCompanies));
                }
                else
                {
                    TempData["ErrorMessage"] = "Company has not been successfully verified!";
                    return View();
                }

            }
            catch
            {
                TempData["ErrorMessage"] = "Company has not been successfully verified!";
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _companyRepository.GetCompanyById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CompanyDTO companyDTO)
        {
            try
            {
                string oldImage = "";
                if (companyDTO.ImageFile != null)
                {
                    if (companyDTO.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException("Image file cannot exceed 1MB");
                    }
                    string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                    string imageName = await _fileService.SaveFile(companyDTO.ImageFile, allowedExtensions);
					companyDTO.CIPCImage = imageName;
                }

				Company company = new Company()
				{
					id = companyDTO.id,
					RegistrationNumber = companyDTO.RegistrationNumber,
					CompanyAddress = companyDTO.CompanyAddress,
					CIPCImage = companyDTO.CIPCImage,
				};

				await _companyRepository.UpdateCompany(company);
                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileService.DeleteFile(oldImage);
                }
                TempData["successMessage"] = "Company is updated successfully!";
                return RedirectToAction(nameof(GetCompanies));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _companyRepository.GetCompanyById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Company company)
        {
            try
            {
                await _companyRepository.DeleteCompany(company);
                _fileService.DeleteFile(company.CIPCImage);
                
                TempData["successMessage"] = "Company is deleted successfully!";
                return RedirectToAction(nameof(GetCompanies));
            }
            catch
            {
                return View();
            }
        }
    }
}
