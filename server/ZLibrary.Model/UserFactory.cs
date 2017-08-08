using System.Collections.Generic;

namespace ZLibrary.Model
{
    public class UserFactory
    {
        public IList<User> CreateCommomnUser()
        {
            var commomnUsers = new List<User>();
            commomnUsers.Add(new User(){ Name = "Alexandre Cunha", Email = "alexandre.cunha@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Caio Maia", Email = "caio.maia@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Daniel Alves", Email = "daniel.alves@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Danillo Magno", Email = "danillo.magno@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Felipe Rios", Email = "felipe.rios@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Fillipe Rosini", Email = "fillipe.rosini@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Filipe Sbragio", Email = "filipe.sbragio@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Marcio Hariki", Email = "marcio.hariki@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Jose Luz", Email = "jose.luz@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Rodrigo Rocha", Email = "rodrigo.rocha@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Leonardo Lombardi", Email = "leonardo.lombardi@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Michele Barros", Email = "michele.barros@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Miguel Scudeller", Email = "miguel.scudeller@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Milton Terra", Email = "milton.terra@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Polyanna Cunha", Email = "polyanna.cunha@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Sergio Taborda", Email = "sergio.taborda@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Bruno Silva", Email = "bruno.silva@zbra.com.br", isAdministrator = false});
            commomnUsers.Add(new User(){ Name = "Ricardo Ushisima", Email = "ricardo.ushisima@zbra.com.br", isAdministrator = false});
            
            
            return commomnUsers;
        }

        public IList<User> CreateAdminUser()
        {
            var adminUser = new List<User>();
            adminUser.Add(new User(){ Name = "Bruno François", Email = "bruno.francois@zbra.com.br", isAdministrator = true});
            adminUser.Add(new User(){ Name = "Cinthya Vieira", Email = "cinthya.vieira@zbra.com.br", isAdministrator = true});
            adminUser.Add(new User(){ Name = "Fabio Augusto Falavinha", Email = "fabio.falavinha@zbra.com.br", isAdministrator = true});
            adminUser.Add(new User(){ Name = "Jayme Preto", Email = "jayme.preto@zbra.com.br", isAdministrator = true});
            adminUser.Add(new User(){ Name = "Jessica Ferreira", Email = "jessica.ferreira@zbra.com.br", isAdministrator = true});
            adminUser.Add(new User(){ Name = "Marina Garibaldi", Email = "marina.garibaldi@zbra.com.br", isAdministrator = true});

            
            return adminUser;
        }
    }
}