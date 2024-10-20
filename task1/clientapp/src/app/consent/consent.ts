import { ConsentDetail } from "./consentdetail";

export class Consent { 
 
    constructor(clientId: string, consentDetails: ConsentDetail[]) {
        this.clientId = clientId;
        this.consentDetails = consentDetails;
    }
 
    clientId: string;
    consentDetails: ConsentDetail[]
}