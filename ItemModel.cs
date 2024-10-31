using Google.Cloud.Firestore;

namespace CologneStore.Models
{
    [FirestoreData]
    public class ItemModel
    {
        [FirestoreProperty("first name")]
        public string FirstName { get; set; }

        [FirestoreProperty("last name")]
        public string LastName { get; set; }
        [FirestoreProperty("email")]
        public string Email { get; set; }
        [FirestoreProperty("address")]
        public string Address { get; set; }
        [FirestoreProperty("payment method")]
        public string PaymentMethod { get; set; }
        [FirestoreProperty("date")]
        public string Date { get; set; }
		[FirestoreProperty("name")]
		public string Name { get; set; }
        [FirestoreProperty("brand")]
        public string Brand { get; set; }
        [FirestoreProperty("price")]
        public double Price { get; set; }
        [FirestoreProperty("image")]
        public string Image { get; set; }
        
    }
}
