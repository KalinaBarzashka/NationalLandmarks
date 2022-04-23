import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LandmarkService } from 'src/app/services/landmark.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent {
  landmarkForm!: FormGroup;

  get name() { return this.landmarkForm.get('name'); }
  get isNationalLandmark() { return this.landmarkForm.get('isNationalLandmark'); }
  get description() { return this.landmarkForm.get('description'); }
  get address() { return this.landmarkForm.get('address'); }
  get latitude() { return this.landmarkForm.get('latitude'); }
  get longitude() { return this.landmarkForm.get('longitude'); }
  get imageUrl() { return this.landmarkForm.get('imageUrl'); }

  constructor(private fb: FormBuilder, private landmarkService: LandmarkService) {
    this.landmarkForm = this.fb.group({
      'name': ['', [Validators.required]],
      'isNationalLandmark': ['', [Validators.required]],
      'description': ['', [Validators.required]],
      'address': ['', [Validators.required]],
      'latitude': ['', [Validators.required]],
      'longitude': ['', [Validators.required]],
      'imageUrl': ['', [Validators.required]]
    });
  }

  createHandler(): void {
    this.landmarkService.create(this.landmarkForm.value).subscribe(res => {
      console.log(res);
    });
  }

}
