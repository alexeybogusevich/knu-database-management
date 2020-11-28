import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public databases: Database[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    console.log(baseUrl)
    http.get<Database[]>(baseUrl + 'api/database').subscribe(result => {
      this.databases = result;
    }, error => console.error(error));
  }
}

interface Database {
  id: string;
  name: string;
}

interface Table {
  id: string;
  name: string;
  databaseId: string;
  databaseName: string;
  schema: string;
}

interface Row {
  id: string;
  tableId: string;
  tableName: string;
  content: string;
}
