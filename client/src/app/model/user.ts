export class User {

    constructor(public id: number,
                public name: string,
                public email: string,
                public isAdministrator: boolean,
                public accessToken: string,
                public userAvatarUrl: string) {
    }
}