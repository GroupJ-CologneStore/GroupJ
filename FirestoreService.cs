using CologneStore.Models;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FirestoreService
{
    FirestoreDb firestoreDb = FirestoreDb.Create("cologne-store");

    // Fetch all documents from the Firestore collection
    public async Task<List<ItemModel>> GetItemsAsync(string collectionName)
    {
        List<ItemModel> items = new List<ItemModel>();

        // Get the collection reference
        CollectionReference collectionRef = firestoreDb.Collection(collectionName);

        // Retrieve all documents in the collection
        QuerySnapshot snapshot = await collectionRef.GetSnapshotAsync();

        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            if (document.Exists)
            {
                // Deserialize the document into the ItemModel
                ItemModel item = document.ConvertTo<ItemModel>();
                items.Add(item);
            }
        }

        return items;
    }

    public async Task<List<ReviewModel>> GetReviewsAsync(string collectionName)
    {
        List<ReviewModel> reviews = new List<ReviewModel>();

        // Get the collection reference
        CollectionReference collectionRef = firestoreDb.Collection(collectionName);

        // Retrieve all documents in the collection
        QuerySnapshot snapshot = await collectionRef.GetSnapshotAsync();

        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            if (document.Exists)
            {
                // Deserialize the document into the ItemModel
                ReviewModel review = document.ConvertTo<ReviewModel>();
                reviews.Add(review);
            }
        }

        return reviews;
    }

    public async Task<List<CustomerModel>> GetCustomersAsync(string collectionName)
    {
        List<CustomerModel> customers = new List<CustomerModel>();

        // Get the collection reference
        CollectionReference collectionRef = firestoreDb.Collection(collectionName);

        // Retrieve all documents in the collection
        QuerySnapshot snapshot = await collectionRef.GetSnapshotAsync();

        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            if (document.Exists)
            {
                // Deserialize the document into the ItemModel
                CustomerModel customer = document.ConvertTo<CustomerModel>();
                customers.Add(customer);
            }
        }

        return customers;
    }
}
