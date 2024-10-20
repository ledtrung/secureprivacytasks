import { Component } from '@angular/core';
import { UserService } from '../../../user.service'
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { User } from '../../../user';

@Component({
  selector: 'user-add',
  standalone: true,
  templateUrl: './add.component.html',
  imports: [ CommonModule, FormsModule, RouterLink ]
})
export class UserAddComponent {
    constructor(private userService: UserService, private router: Router) {}

    onSubmit(form: NgForm) {
        let user = new User(form.value.name, form.value.email, form.value.address, form.value.dob);
        this.userService.addUser(user).subscribe(_ => { 
            this.router.navigate(["/users"]);
        });
    }
}
