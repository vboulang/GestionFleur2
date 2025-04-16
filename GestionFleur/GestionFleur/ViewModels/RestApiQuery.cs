using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

//Pour aller chercher 30 entree dans l<api users?limit=30&select=id,firstName,lastName,username,password
//aller chercher les format a entrer selon le type de la demande
//ajouter la categorie type a chaque entree
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

		public async Task<List<Models.Utilisateur>> GetUtilisateursAsync(string path)
		{
			HttpResponseMessage response = await _client.GetAsync(path);
			if(response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				List<Models.Utilisateur> utilisateurs = JsonConvert.DeserializeObject<List<Models.Utilisateur>>(data);
				return utilisateurs;
			}
			return new List<Models.Utilisateur>();
		}

		public List<Models.Utilisateur> GetUtilisateurs(string path)
		{
			List<Models.Utilisateur> utilisateurs = new List<Models.Utilisateur>();
			try
			{
				Task<List<Models.Utilisateur>> utilisateur = Task.Run(async () => await GetUtilisateursAsync(path));
				utilisateur.Wait();
				utilisateurs = utilisateur.Result;
			}
			catch (Exception e) { Console.WriteLine(e); }

			return utilisateurs;

		}
	}
}
