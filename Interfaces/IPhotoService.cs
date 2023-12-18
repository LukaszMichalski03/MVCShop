namespace LoginRegisterIdentity.Interfaces
{
	public interface IPhotoService
	{
		Task<string> AddPhotoAsync(IFormFile file);
		Task<bool> DeletePhotoAsync(string imageUrl);
	}
}
