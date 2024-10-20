import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { ConsentComponent } from './consent/consent.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CookieService } from 'ngx-cookie-service';
import { ConsentSrvice } from './consent/consent.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [MatDialogModule, RouterOutlet, RouterLink],
  providers: [CookieService],
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  constructor(private dialog: MatDialog, private consentService: ConsentSrvice) {
  }

  ngOnInit(): void {
    let consentCookie = this.consentService.getConsentCookie();
    if (!consentCookie) {
      this.openPopup();
    }
  }

  openPopup() {
    let dialogRef = this.dialog.open(ConsentComponent, {
    });
  }
}
