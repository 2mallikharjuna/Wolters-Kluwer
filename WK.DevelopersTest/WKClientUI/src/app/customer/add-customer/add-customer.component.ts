import { Component, OnInit } from '@angular/core';  
import { CustomerService } from 'src/app/services/customer.service';  
import { FormControl, FormGroup, Validators, FormBuilder, ReactiveFormsModule } from '@angular/forms';  
import { Router } from '@angular/router';  
import { ToastrService } from 'ngx-toastr';  
  
@Component({  
  selector: 'app-add-customer',  
  templateUrl: './add-customer.component.html',  
  styleUrls: ['./add-customer.component.css']  
})  

export class AddCustomerComponent implements OnInit {  
  submitted: boolean= false;  
  customerForm: any;  
    
  constructor(private formBuilder: FormBuilder,private toastr: ToastrService,private customerService: CustomerService, private router:Router) { }  
  
  ngOnInit(): void {  
    this.customerForm = this.formBuilder.group({       
      "FirstName": ["", Validators.required],  
      "LastName": ["", Validators.required],  
      "DateOfBirth": ["", Validators.required],
      "EmailId": ["", Validators.required],  
      "Gender": ['', Validators.required],  
      "Address": ["", Validators.required],  
      "MobileNo": ["", Validators.required],  
      "PinCode": ["", Validators.required]  
    });  
  }  
  onSubmit() {  
    this.submitted = true;  
    if (this.customerForm.invalid) {  
      return;  
    }  

    this.customerService.addCustomer(this.customerForm.value)  
      .subscribe( data => {  
        this.toastr.success("success", data.toString());  
        this.router.navigate(['customers']);  
      });  
  }  

  Cancel()  
  {  
    this.router.navigate(['customers']);  
  }  

}  
