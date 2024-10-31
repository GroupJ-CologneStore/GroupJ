using Google.Cloud.Firestore;

namespace CologneStore.Models
{
    [FirestoreData]
    public class ReviewModel
    {
        [FirestoreProperty("email")]
        public string User { get; set; }

        [FirestoreProperty("cologne name")]
        public string Cologne { get; set; }
        [FirestoreProperty("review message")]
        public string Review { get; set; }
        [FirestoreProperty("rating")]
        public string Rating { get; set; }
       
    }
}
