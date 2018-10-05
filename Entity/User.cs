using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Test_API.Entity
{

    public class User
    {
        [BsonId]
        public ObjectId Id {get; set;}
        public int userId {get; set;}

        [Required(ErrorMessage = "login requis")]
        [EmailAddress(ErrorMessage="Le login doit être une adresse mail")]
        public string login {get; set;}

        [RegularExpression("^(?=.*[A-Za-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@.,;:*/_])\\S{8,}$", ErrorMessage="Le mot de passe doit contenir au minimum 1 majuscule, 1 minuscule, 1 chiffre et 1 caractère spécial ($@.,;:*/_)")]
        public string password {get; set;}

        public Image userImage {get; set;}
    }


}