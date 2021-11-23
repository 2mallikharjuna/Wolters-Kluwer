import {Injectable} from '@angular/core';
import {Customer} from '../model/customer';
import {HttpClientBase} from './http-client-base.service';
import { Observable} from 'rxjs';

@Injectable({ providedIn: 'root'})

export class CustomerService {  
  constructor(private httpClient: HttpClientBase) { }

  editCustomer(customer: Customer) {
    return this.httpClient.put('UpdateCustomer/', customer);
  }

  getCustomers(): Observable<Customer[]> {
    return this.httpClient.get<Customer[]>('Customers/ListAll');
  }

  addCustomer(customer: Customer): Observable<string> {    
    return this.httpClient.post<string,  any>(`Customers/`, customer);
  }

  updateCustomer(customer: Customer): Observable<string> {    
    return this.httpClient.put<string, any>(`Customers/`, customer);
  }

  updateCustomers(customers: Customer[]): Observable<string> {    
    return this.httpClient.put<string, any>(`Customers/UpdateCustomers/`, customers);
  }
  
  deleteCustomer(CustomerId: string): Observable<string> {    
    return this.httpClient.delete<string>(`/` + CustomerId);
  }
  
}
