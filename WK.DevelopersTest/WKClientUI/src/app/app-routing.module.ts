import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddCustomerComponent } from './customer/add-customer/add-customer.component';
import { CustomerComponent } from './customer/customer.component';


const routes: Routes = [
  { path: 'addCustomer', component: AddCustomerComponent },
  { path: 'customers', component: CustomerComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
