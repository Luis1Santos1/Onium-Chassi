import { Component} from '@angular/core';
import { VehicleDetailService } from 'src/app/shared/vehicle-detail.service';
import { NgForm } from "@angular/forms";
import { VehicleDetail } from 'src/app/shared/vehicle-detail.model';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-vehicle-detail-form',
  templateUrl: './vehicle-detail-form.component.html',
  styles: [
  ]
})
export class VehicleDetailFormComponent {

  constructor(public service: VehicleDetailService, private toastr: ToastrService) {
  }

  onSubmit(form:NgForm){
    console.log('ID antes da verificação:', this.service.formData.id);
    this.service.formSubmitted = true
    if(form.valid){
      if(this.service.formData.id == 0)
      this.insertRecord(form)
    else
      this.updateRecord(form)
    }

  }
  insertRecord(form: NgForm){
    this.service.postVehicleDetail()
    .subscribe({
      next: res => {
        this.service.list = res as VehicleDetail[]
        this.service.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Inserted successfully', 'Vehicle Register')

      },
      error: err => { console.log(err) }
    })
  }
  updateRecord(form: NgForm){
    this.service.putVehicleDetail()
    .subscribe({
      next: res => {
        this.service.list = res as VehicleDetail[]
        this.service.resetForm(form);
        this.service.refreshList();
        this.toastr.info('Updated successfully', 'Vehicle Register')

      },
      error: err => { console.log(err) }
    })
  }
}
