import { Component, OnInit } from '@angular/core';
import { VehicleDetailService } from '../shared/vehicle-detail.service';
import { VehicleDetail } from '../shared/vehicle-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styles: [
  ]
})
export class VehicleDetailsComponent implements OnInit {
  showData: boolean = false;
  constructor(public service: VehicleDetailService, private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.service.refreshList();
  }

  ShowData() {
    this.showData = !this.showData;
  }

  populateForm(selectedRecord:VehicleDetail){
    this.service.formData = Object.assign({},selectedRecord);
  }

  onDelete(id:number){
    if(confirm('Are you sure to delete this record?'))
    this.service.deleteVehicleDetail(id)
    .subscribe({
      next: res => {
        this.service.list = res as VehicleDetail[]
        this.service.refreshList();
        this.toastr.error('Deleted successfully', 'Vehicle Register')

      },
      error: err => { console.log(err) }
    })
  }


}
