import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConsentType } from './consenttype';
import { Consent } from './consent';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class ConsentSrvice {
  baseUrl: string = "http://localhost:5048/";

  constructor(private http: HttpClient, private cookieService: CookieService) { }

  public getConsentCookie(): string { 
    let consentCookie = this.cookieService.get("spconsent");
    return consentCookie;
  }

  public setConsentCookie(value: string) { 
    this.cookieService.set("spconsent", value);
  }

  public getConsentTypes() {
    return this.http.get<ConsentType[]>(this.baseUrl + 'consent/types');
  }

  public getConsent(id: string) {
    return this.http.get<Consent>(this.baseUrl + 'consent/' + id);
  }

  public addOrUpdateConsent(consent: Consent) { 
    const headers = { 'content-type': 'application/json'}  
    const body = JSON.stringify(consent);
    this.setConsentCookie(body);
    return this.http.post(this.baseUrl + 'consent', body, { 'headers': headers });
  }
}