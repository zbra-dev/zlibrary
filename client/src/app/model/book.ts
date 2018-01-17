import {Publisher} from "./publisher"
import {Author} from "./author"
import {Isbn} from "./isbn"

export class Book {
    constructor(public id: number, public title: string, public publisher: Publisher, public authors: Author[],  public isbn: Isbn, public synopsis: string, public publicationYear: number, public numberOfCopies: number, public coverImageKey: string, public created: Date) {
    }
}
