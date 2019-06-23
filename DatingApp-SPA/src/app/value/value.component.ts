import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {
  values: any;

  constructor(
    private http: HttpClient
  ) { }

  ngOnInit() {
    this.getValues();
  }

  getValues() {
    // tslint:disable-next-line:max-line-length
    this.http.get('http://localhost:5000/api/values').subscribe(
      response => {
        this.values = response;
      },
      error => {
        console.log(error);
      }
    ); // get returns an Observable - basically a stream of data coming back from the server. Must subscribe to retrieve data.
  }

}
