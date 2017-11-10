export class User {
    public id: number;
    public username: string;
    public name: string;
    public token: string;

    constructor(init?: Partial<User>) {
        Object.assign(this, init);
    }
}
