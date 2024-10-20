export class ConsentDetail { 
 
    constructor(type: string, consented: boolean) {
        this.type = type;
        this.consented = consented;
    }
 
    type: string;
    consented: boolean;
}