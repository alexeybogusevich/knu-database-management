import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SignalRService } from '../services/signal-r.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})

export class FetchDataComponent implements OnInit {
  public databases: Database[];
  public httpClient: HttpClient;
  public domain: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, public signalRService: SignalRService) {
    this.fetchDatabases(http, baseUrl);
    this.httpClient = http;
    this.domain = baseUrl;
  }

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListener(this);
  }

  public fetchDatabases(http: HttpClient, baseUrl: string) {
    console.log(baseUrl)
    http.get<Database[]>(baseUrl + 'api/database').subscribe(result => {
      console.log(result);
      console.log(this);
      this.databases = result;
    }, error => console.error(error));
  }

}

interface Database {
  id: string;
  name: string;
}

