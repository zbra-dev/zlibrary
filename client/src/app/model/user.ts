export class User {

    constructor(public id: number,
                public name: string,
                public email: string,
                public isAdministrator: string,
                public accessToken: string,
                public userAvatarUrl: string) {}
}