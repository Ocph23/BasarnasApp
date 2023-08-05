namespace BasarnasMobilaApp.Models
{
    public class Pelapor : BaseNotify
    {
        private int id;
        private string name;
        private string email;
        private string phoneNumber;
        private string address;
        private string photo;
        private string userId;
        private string confirmPassword;
        private byte[] photoData;
        private string password;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        public string? Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        public string? Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }
        public string? PhoneNumber
        {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }
        public string? Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }
        public string? Photo
        {
            get { return photo; }
            set { SetProperty(ref photo, value); }
        }
        public string? UserId
        {
            get { return userId; }
            set { SetProperty(ref userId, value); }
        }
        public string? Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
        public string? ConfirmPassoword
        {
            get { return confirmPassword; }
            set { SetProperty(ref confirmPassword, value); }
        }
        public byte[]? PhotoData
        {
            get { return photoData; }
            set { SetProperty(ref photoData, value); }
        }

    }
}
