import { Component, OnInit, ViewChild } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { Customer } from '../model/customer';
import { ColDef, GridApi, ColumnApi } from 'ag-grid-community';
import { CustomerService } from '../services/customer.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Gender } from '../model/enums';
import * as moment from 'moment'

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})



export class CustomerComponent implements OnInit {
  // row data and column definitions  
  public customers: Customer[];
  public columnDefs: ColDef[];
  // gridApi and columnApi  
  private gridApi: GridApi;
  private columnApi: ColumnApi;

  @ViewChild('agGrid') agGrid: AgGridAngular;

  showSaveAndCancel = true;
  showAddButton = true;
  showDeleteButton = true;
  showSaveButton = true;
  showCancelButton = true;

  addedRowIndexList: number[] = [];
  addedRowDataList: any[] = [];
  itemsToUpdate: any[] = [];
  itemsToDelete: any[] = [];

  constructor(private customerService: CustomerService, private router: Router, private toastr: ToastrService) {
    this.columnDefs = this.createColumnDefs();
  }

  ngOnInit() {
    this.customerService.getCustomers().subscribe(data => {
      this.customers = data
    });
    
    this.showSaveButton = false;
    this.showDeleteButton = true;   
  }

  // one grid initialisation, grap the APIs and auto resize the columns to fit the available space  
  onGridReady(params: any): void {
    this.gridApi = params.api;
    this.columnApi = params.columnApi;
    this.gridApi.sizeColumnsToFit();
  }

  // create column definitions  
  private createColumnDefs() {
    return [{
      headerName: 'First Name',
      field: 'firstName',
      filter: true,
      enableSorting: true,
      editable: true,
      sortable: true
    },
    {
      headerName: 'Last Name',
      field: 'lastName',
      filter: true,
      enableSorting: true,
      editable: true,
      sortable: true
    },
    {
      headerName: 'Email Id',
      field: 'emailId',
      filter: true,
      editable: true,
      sortable: true,
      cellRenderer: '<a href="edit-user">{{EmailId}}</a>'
    },
    {
      headerName: 'Gender',
      field: 'gender',
      filter: true,
      type: 'numberColumn',
      sortable: true,
      editable: true,
      cellRenderer: this.cellRederer,
      cellEditorSelector: this.cellEditorSelector
    },
    {
      headerName: "Birth Date",
      field: "dateOfBirth",
      editable: true,
      valueGetter: this.dateValueGettter
    },
    {
      headerName: 'Address',
      field: 'address',
      filter: true,
      editable: true,
      sortable: true

    },
    {
      headerName: 'Mobile',
      field: 'mobileNo',
      filter: true,
      editable: true
    }]
  }
  status: any;

  //Update customer  
  editRow() {
    debugger;

    this.customerService.updateCustomer(this.itemsToUpdate[0]).subscribe(data => {
      this.toastr.success("success", data);
      this.ngOnInit();
    });
    this.showSaveButton = false;
  }

  //Delete customer  
  deleteCustomer() {
    debugger;
    var selectedRows = this.gridApi.getSelectedRows();
    if (selectedRows.length == 0) {
      this.toastr.error("error", "Please select a User for deletion");
      return;
    }

    this.itemsToDelete.push(...selectedRows);
    this.gridApi.applyTransaction({ remove: selectedRows });
    this.showDeleteButton = false;
    this.showSaveButton = true;
  }

  cellEditorSelector(params: any) {

    if (params.colDef.field === 'gender') {
      return {
        component: 'agRichSelect',
        params: { gender: ['Male', 'Female'] }
      };
    }
    return undefined;
  }

  cellRederer(params: any) {
    return (params.data.gender !== null && params.data.gender !== undefined)
      ? Gender[params.data.gender] : 'not found';

  }
  genderValueGetter(params: any) {
    return Number(params.data.gender);
  }

  dateValueGettter(params: any) {

    var dateMomentObject = moment(params.data.dateOfBirth.toString(), "YYYY-MM-DD");
    return dateMomentObject.format("YYYY-MM-DD");
  }
  createCustomer() {
    this.router.navigate(['addCustomer']);
  }

  saveAll() {
    if (this.itemsToUpdate.length > 0) {
      this.customerService.updateCustomers(this.itemsToUpdate).subscribe(data => {
        this.toastr.success("success", data);
        this.ngOnInit();
        this.itemsToUpdate.splice(0, this.itemsToUpdate.length);
      });
      this.gridApi.applyTransaction({ update: this.itemsToUpdate });
      this.showSaveButton = false;
    }

    if (this.itemsToDelete.length > 0) {
      this.customerService.deleteCustomer(this.itemsToDelete[0].id).subscribe(data => {
        this.toastr.success("success", data);
        this.ngOnInit();
        this.itemsToUpdate.splice(0, this.itemsToDelete.length);
        this.showDeleteButton = true;
        this.showSaveButton = false;
      });

    }
  }

  formatName(params: any) {
    var name = params.value;
    var firstChar = name.slice(0, 1).toUpperCase();
    return firstChar + name.slice(1);
  }

  cancelAll() {
    this.customerService.getCustomers().subscribe(data => {
      this.customers = data
    });
    this.showSaveButton = false;
    this.showDeleteButton = true;    
  }

  onCellValueChanged(params: any) {
    console.log('Data after change is', params.data);

    if (this.itemsToUpdate.length === 0) {
      this.itemsToUpdate.push(params.data);
    }
    else {
      let itemIndex = this.itemsToUpdate.findIndex(item => item.id === params.data.id);
      params.data.gender = Number(params.data.gender);//converting the string to enum type
      this.itemsToUpdate[itemIndex] = params.data;
    }
    this.showSaveButton = true;
  }
}



