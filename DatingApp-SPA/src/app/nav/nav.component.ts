import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(
    public authService: AuthService,
    private alertifyService: AlertifyService
  ) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model)
    .subscribe(next => {
      this.alertifyService.success('Logged In!');
      // console.log('Logged In.');
    }, error => {
      this.alertifyService.error(error);
      // console.log(error);
    });
  }

  loggedIn() {
    return this.authService.loggedIn(); // check token for expiry date.
  }

  logout() {
    localStorage.removeItem('Token');
    this.alertifyService.message('Logged Out.');
  }

}
