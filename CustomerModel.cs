using Google.Cloud.Firestore;

namespace CologneStore.Models
{
    [FirestoreData]
    public class CustomerModel
    {
        [FirestoreProperty("first name")]
        public string FirstName { get; set; }

        [FirestoreProperty("last name")]
        public string LastName { get; set; }
        [FirestoreProperty("email")]
        public string Email { get; set; }
        [FirestoreProperty("phone number")]
        public string Phone { get; set; }
        [FirestoreProperty("ID")]
        public string ID { get; set; }
        
    }
}
