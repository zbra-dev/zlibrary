using System.Collections.Generic;
using System.Linq;

namespace ZLibrary.Model
{
    public class UserFactory
    {
        public static IList<User> CreateUsers()
        {
            return CreateCommonUsers().Concat(CreateAdminUsers()).ToList();
        }

        private static IList<User> CreateCommonUsers()
        {
            var commonUsers = new List<User>();
            commonUsers.Add(new User() { Name = "Alexandre Cunha", Email = "alexandre.cunha@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Danillo Magno", Email = "danillo.magno@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Fillipe Rosini", Email = "fillipe.rosini@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Filipe Sbragio", Email = "filipe.sbragio@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Gabriel Garcia", Email = "gabriel.garcia@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Jose Luz", Email = "jose.luz@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Rodrigo Rocha", Email = "rodrigo.rocha@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Leonardo Lombardi", Email = "leonardo.lombardi@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Michele Barros", Email = "michele.barros@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Miguel Scudeller", Email = "miguel.scudeller@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Milton Terra", Email = "milton.terra@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Polyanna Cunha", Email = "polyanna.cunha@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Sergio Taborda", Email = "sergio.taborda@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Bruno Silva", Email = "bruno.silva@zbra.com.br", IsAdministrator = false });
            commonUsers.Add(new User() { Name = "Ricardo Ushisima", Email = "ricardo.ushisima@zbra.com.br", IsAdministrator = false });
            return commonUsers;
        }

        private static IList<User> CreateAdminUsers()
        {
            var adminUsers = new List<User>();
            adminUsers.Add(new User() { Name = "Bruno François", Email = "bruno.francois@zbra.com.br", IsAdministrator = false });
            adminUsers.Add(new User() { Name = "Cinthya Vieira", Email = "cinthya.vieira@zbra.com.br", IsAdministrator = true, UserAvatarUrl="https://a.slack-edge.com/7fa9/img/avatars/ava_0007-512.png" });
            adminUsers.Add(new User() { Name = "Fabio Augusto Falavinha", Email = "fabio.falavinha@zbra.com.br", IsAdministrator = true });
            adminUsers.Add(new User() { Name = "Jayme Preto", Email = "jayme.preto@zbra.com.br", IsAdministrator = true });
            adminUsers.Add(new User() { Name = "Jessica Ferreira", Email = "jessica.ferreira@zbra.com.br", IsAdministrator = true });
            adminUsers.Add(new User() { Name = "Marina Garibaldi", Email = "marina.garibaldi@zbra.com.br", IsAdministrator = true });
            return adminUsers;
        }
    }
}