using GestionFleur.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GestionFleur.ViewModels
{
	internal class RestApiQuery
	{
		private HttpClient _client;

		public RestApiQuery()
		{
			_client = new HttpClient();
			_client.BaseAddress = new Uri("https://dummyjson.com/");
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<Models.UserToDeserialize> GetUtilisateursAsync(string path)
		{
			HttpResponseMessage response = await _client.GetAsync(path);
			if(response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				Models.UserToDeserialize utilisateurs = JsonConvert.DeserializeObject<Models.UserToDeserialize> (data);
				return utilisateurs;
			}
			return new Models.UserToDeserialize();
		}

		public List<Models.Utilisateur> GetUtilisateurs(string path)
		{
			List<Models.Utilisateur> utilisateurs = new List<Models.Utilisateur>();
			try
			{
				Task<Models.UserToDeserialize> utilisateur = Task.Run(async () => await GetUtilisateursAsync(path));
				utilisateur.Wait();
				utilisateurs = utilisateur.Result.users;
			}
			catch (Exception) { }

			return utilisateurs;

			
		}
	}
}
