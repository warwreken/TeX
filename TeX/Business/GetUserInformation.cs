using System.DirectoryServices;
using TeX.Models;

namespace TeX.Business
{
    public class GetUserInformation
    {
        public static string GetInformation(WebHook wh)
        {
            string results = string.Empty;
            var dsDirectoryEntry = new DirectoryEntry("LDAP://AGDOMAIN");

            var dsSearch = new DirectorySearcher(dsDirectoryEntry) { Filter = "(&(objectClass=user)(CN=" + wh.result.parameters["nome"].Replace(' ', '*') + "))" };

            var dsResults = dsSearch.FindAll();
            if (dsResults.Count == 0)
            {
                return "Não encontrei informações no meu vasto banco de dados... Acho que alguém não sabe o que quer...";
            }
            else
            {
                if (dsResults.Count > 1)
                {
                    string nomes = "Eu encontrei " + dsResults.Count + " nomes. Qual das pessoas você quer saber?\n";
                    foreach (SearchResult nome in dsResults)
                    {
                        nomes += nome.Properties["displayname"][0] + " | ";
                    }
                    nomes = nomes.Remove(nomes.Length - 2);
                    return nomes;
                }
                else
                {
                    var phone = dsResults[0].Properties["mobile"][0];
                    var phone2 = dsResults[0].Properties["ipphone"][0];
                    var name = dsResults[0].Properties["displayname"][0];
                    var department = dsResults[0].Properties["department"][0];
                    var title = dsResults[0].Properties["title"][0];
                    var login = dsResults[0].Properties["mail"][0];

                    return "As informações do usuário " + name + " são as seguintes:\n\nNome: " + name + "\nE-mail: " + login + "\nCargo: " + title + "\nDepartamento: " + department + "\nTelefone de Trabalho: " + phone2 + "\nCelular: " + phone;
                }
            }
        }
    }
}