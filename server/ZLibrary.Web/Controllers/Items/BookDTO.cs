using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ZLibrary.Model;

namespace ZLibrary.Web.Controllers.Items
{
    public class BookDto
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "publisher")]
        public PublisherDto Publisher { get; set; }
        
        [DataMember(Name = "authors")]
        public AuthorDto[] Authors { get; set; }
        
        [DataMember(Name = "isbn")]
        public string Isbn { get; set; }

        [DataMember(Name = "coverImageKey")]
        public Guid CoverImageKey { get; set; }
        
        [DataMember(Name = "synopsis")]
        public string Synopsis { get; set; }
        
        [DataMember(Name = "publicationYear")]
        public int PublicationYear { get; set; }

        [DataMember(Name = "numberOfCopies")]
        public int NumberOfCopies { get; set; }

        [DataMember(Name = "reservations")]
        public IEnumerable<ReservationResultDto> Reservations { get; set; }

        [DataMember(Name = "edition")]
        public string Edition { get; set; }
    }
}