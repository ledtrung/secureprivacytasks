import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ConsentSrvice } from './consent.service';
import { Consent } from './consent';
import { ConsentDetail } from './consentdetail';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-consent',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './consent.component.html'
})
export class ConsentComponent implements OnInit {
  consent: Consent;

  constructor(public dialogRef: MatDialogRef<ConsentComponent>, private consentService: ConsentSrvice) { }

  ngOnInit(): void {
    this.refreshConsentForm();
  }

  refreshConsentForm() {
    let consentCookie = this.consentService.getConsentCookie();
    if (consentCookie) {
      this.consent = JSON.parse(consentCookie) as Consent;
    }
    else {
      this.consentService.getConsentTypes().subscribe(cts => {
        var consentDetails: ConsentDetail[] = [];
        cts.forEach(type => {
          consentDetails.push(new ConsentDetail(type.type, type.mandatory));
        });
        this.consent = new Consent(self.crypto.randomUUID(), consentDetails);
      });
    }
  }

  acceptCookies() {
    console.log(this.consent);
    this.submitConsent();
  }

  close() {
    this.submitConsent();
  }

  submitConsent() {
    this.consentService.addOrUpdateConsent(this.consent).subscribe(data => {
      this.dialogRef.close();
    });
  }
}
