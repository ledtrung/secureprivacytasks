export class ConsentType { 
 
    constructor(type: string, mandatory: boolean) {
        this.type = type;
        this.mandatory = mandatory;
    }
 
    type:string;
    mandatory: boolean;
}