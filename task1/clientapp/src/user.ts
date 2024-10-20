export class User { 
 
    constructor(name: string, email: string, address: string, dob: Date) {
        this.name = name;
        this.email = email;
        this.address = address;
        this.dob = dob
    }
 
    id:string;
    name: string;
    email: string;
    address: string;
    dob: Date;
 
}