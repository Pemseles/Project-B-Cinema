using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.Linq;
using static System.Console;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace ConsoleApp1
{
    public class ReviewCreation : Utility  // Inherits from Abstract Class
    {
        public static string ReviewPath = Path.GetFullPath(@"Reviews.json");
        public static Accounts accountObj = new Accounts();
        /// <summary>
        /// Generates a mew ID based on the Review Entries in Reviews.json
        /// </summary>
        /// <returns>A newly generated ID (int)</returns>
        public static int GenerateID()
        {
            /// Checks opens the json file given as parameter and creates a new ID based on previous ID's from the Json.
            var jsonData = File.ReadAllText(ReviewPath);
            var reviews = JsonConvert.DeserializeObject<List<Review>>(jsonData);

            int newID = 0;
            try
            {
                foreach (Review element in reviews) { newID = element.ID; };
                return newID + 1;
            }
            catch (Exception) // Execute if there are no entries in the JSON
            {
                return 0;
            }
        }

        /// <summary>
        /// Adds a new Review
        /// </summary>
        /// <param name="uid">The User's ID (int)</param>
        /// <param name="tagID">Tag ID(int) - 0: Movie Review, 1: Theatherhall Review, 2: CineScoop Review</param>
        /// <param name="revID">The ID(int) for the Movie or Theatherhall depending on tagID</param>
        /// <param name="author">"The (display) name of the user</param>
        /// <param name="title">(string) Title of the review</param>
        /// <param name="description">(string) Review Description</param>
        /// <param name="stars">The amount of stars given as an integer</param>
        /// <param name="uploadDate">A DateTime (string)</param>
        public static void AddReview(int uid, int tagID, int revID, string author, string title, string description, int stars, string uploadDate)
        {
            /// Requires All and only Correct Parameters to Insert to the JSON File.
            // Read existing json data
            var jsonData = File.ReadAllText(ReviewPath);
            // De-serialize to object or create new list
            var reviews = JsonConvert.DeserializeObject<List<Review>>(jsonData)
                                  ?? new List<Review>();
            // Adds a new Review
            reviews.Add(new Review()
            {
                ID = GenerateID(),
                UID = uid, // Default 1
                TagID = tagID,
                RevID = revID,
                Author = author,
                Title = title,
                Description = description,
                Stars = stars,
                UploadDate = uploadDate,
                Active = true,
            });

            // Update json data string
            jsonData = JsonConvert.SerializeObject(reviews, Formatting.Indented);
            // serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(ReviewPath, jsonData);
        }

        /// <summary>
        /// Lists all Reviews Deserialized from Reviews.json
        /// </summary>
        /// <returns>List with all Review objects</returns>
        public static List<Review> GetAllReviews()
        {
            /// Deletes a Review, with given ID.
            var jsonData = System.IO.File.ReadAllText(ReviewPath);
            var reviews = JsonConvert.DeserializeObject<List<Review>>(jsonData);
            return reviews;
        }


        /// <summary>
        /// Lists all Reviews of Guests and Accounts that are active. Deserialized from Reviews.json
        /// </summary>
        /// <returns>List with all Review objects</returns>
        public static List<Review> GetActiveReviews()
        { 
            var jsonData = System.IO.File.ReadAllText(ReviewPath);
            var reviews = JsonConvert.DeserializeObject<List<Review>>(jsonData);
            var accountJsonData = System.IO.File.ReadAllText(@"Accounts.json");
            var accounts  = JsonConvert.DeserializeObject<List<Review>>(accountJsonData);
            List<Review> FilteredReviews = new List<Review>();

            foreach (Review review in reviews )
            {
                if (review.UID == -1)
                {
                    FilteredReviews.Add(review);
                } else {
                    if (accountObj.GetActiveStatus(review.UID)){
                        FilteredReviews.Add(review);
                    }
                }
            }
            return FilteredReviews;
        }

        /// <summary>
        /// Delete Method
        /// Takes a review id as parameter and deletes the review with the matching id.
        /// </summary>
        /// <param name="id">The ID of the Review that needs to be deleted</param>
        public static void DeleteReview(int id)
        {
            /// Deletes a Review, with given ID.
            var jsonData = File.ReadAllText(ReviewPath);
            var reviews = JsonConvert.DeserializeObject<List<Review>>(jsonData);
            List<Review> FilteredReviews = new List<Review>();
            foreach (Review review in reviews)
            {
                if (id == review.ID)
                {
                    FilteredReviews.Remove(review);
                }
            }

            // Update json data string
            jsonData = JsonConvert.SerializeObject(reviews, Formatting.Indented);
            // Serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(ReviewPath, jsonData);
        }

        /* Filtering
         * The following methods are for filtering:
         * - Filter on reviews for certain movies.
         * - Filter on users, or just on tags.
         */

        /// <summary>
        /// Param Overload 1 - On both TagID and ID of given Subject
        /// </summary>
        /// <param name="tagID">Tag ID(int) - 0: Movie Review, 1: Theatherhall Review</param>
        /// <param name="revID">The ID(int) for the Movie or Theatherhall depending on tagID</param>
        /// <returns>A Filtered version of the Review List</returns>
        public static List<Review> FilterReviews(int tagID, int revID)
        { /// Filters all reviews with given parameters
            var jsonData = System.IO.File.ReadAllText(ReviewPath);
            var AllReviews = JsonConvert.DeserializeObject<List<Review>>(jsonData);
            // Create new list with only filtered Reviews
            List<Review> FilteredReviews = new List<Review>();
            foreach (Review review in AllReviews)
            {
                if (tagID == review.TagID && revID == review.RevID)
                {
                    FilteredReviews.Add(review);
                }
            }
            return FilteredReviews;
        }
        /// <summary>
        /// Param Overload 2 - Only Filter on tagID
        /// </summary>
        /// <param name="tagID">Tag ID(int) - 0: Movie Review, 1: Theatherhall Review, 2: CineScoop Review</param>
        /// <returns>A Filtered version of the Review List</returns>
        public static List<Review> FilterReviews(int tagID)
        { /// Filters all reviews with given parameters
            var jsonData = System.IO.File.ReadAllText(ReviewPath);
            var AllReviews = JsonConvert.DeserializeObject<List<Review>>(jsonData);
            // Create new list with only filtered Reviews
            List<Review> FilteredReviews = new List<Review>();
            foreach (Review review in AllReviews)
            {
                if (tagID == review.TagID)
                {
                    FilteredReviews.Add(review);
                }
            }
            return FilteredReviews;
        }

        /// <summary>
        /// Filter all Reviews of a user.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static List<Review> GetUserReviews(int uid)
        { /// Filters all reviews with given parameters
            var jsonData = System.IO.File.ReadAllText(ReviewPath);
            var AllReviews = JsonConvert.DeserializeObject<List<Review>>(jsonData);
            // Create new list with only filtered Reviews
            List<Review> FilteredReviews = new List<Review>();
            foreach (Review review in AllReviews)
            {
                if (uid == review.UID)
                {
                    FilteredReviews.Add(review);
                }
            }
            return FilteredReviews;
        }
    }
}

