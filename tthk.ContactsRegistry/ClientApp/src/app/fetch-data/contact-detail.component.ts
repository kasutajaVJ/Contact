import { ActivatedRoute, Router } from '@angular/router';
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { isDate } from 'util';

@Component({
  selector: 'contact-detail',
  templateUrl: './contact-detail.component.html'
})
export class ContactDetailComponent {

  public loading = true;

  public contacts: IContact[];

  public model = {
    phoneNumbers: [],
    emails: []
  } as IContact;

  public phoneTypes = ["work", "home"];

  public emailTypes = ["work", "spam", "fun"];

  constructor(private _http: HttpClient, @Inject('BASE_URL') private _baseUrl: string, private _route: ActivatedRoute, private _router: Router) {

  }

  ngOnInit() {
    this.model.id = this._route.snapshot.params['id'];

    if (this.model.id) {
      this._http.get<IContact>(this._baseUrl + `api/Contacts/${this.model.id}`)
        .subscribe(result => {
          Object.assign(this.model, result[0]);
          this.loading = false;
        }, error => console.error(error));
    }
    else {
      this.loading = false;
    }
  }

  public addNumber() {
    this.model.phoneNumbers.push({} as IPhoneNumber);
  }

  public removeNumber(i) {
    this.model.phoneNumbers.splice(i, 1);
  }

  public addEmail() {
    this.model.emails.push({} as IEmail);
  }

  public removeEmail(i) {
    this.model.emails.splice(i, 1);
  }
  
  public save() {

    if (this.model.id) {

      const updateData: IAddEasyContact = {
        id: this.model.id,
        name: this.model.name,
        phoneNumber: this.model.phoneNumber,
        email: this.model.email,
        initials: this.model.initials
      };

      console.log(updateData);

      this._http.post<any>(this._baseUrl + 'api/Contacts', updateData)
        .subscribe(result => {
          this._router.navigate(["fetch-data"]);
        }, error => console.error(error));

    }
    else {

      this._http.post<any>(this._baseUrl + `api/Contacts/add`, this.model)
        .subscribe(result => {
          this._router.navigate(["fetch-data"]);
        }, error => console.error(error));
    }
  }

}

interface IContact {
  id: string;
  name: string;
  initials: string;
  phoneNumber: string;
  email: string;
  phoneNumbers: IPhoneNumber[],
  emails: IEmail[]
}

interface IEmail {
  email: string;
  isDefault: boolean;
  type: string;
}

interface IPhoneNumber {
  number: string;
  isDefault: boolean;
  type: string;
}

interface IContact {
  id: string,
  name: string;
  defaultPhoneNumber: string;
  defaultEmail: string;
}

interface IAddEasyContact {
  id: string,
  name: string,
  phoneNumber: string,
  email: string,
  initials: string
}
