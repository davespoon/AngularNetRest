import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {Router} from "@angular/router";
import {DataService} from "../../core/services/data.service";
import {DataFilterService} from "../../core/services/data-filter.service";

@Component({
  selector: 'board-game-image',
  templateUrl: './board-game-image.component.html',
  styleUrls: ['./board-game-image.component.css']
})
export class BoardGameImageComponent implements OnInit {

  SERVER_URL = "/api/boardGames/images/1";
  uploadForm: FormGroup;

  constructor(private router: Router,
              private dataService: DataService,
              private dataFilter: DataFilterService,
              private formBuilder: FormBuilder,
              private httpClient: HttpClient) {
  }

  ngOnInit() {
    this.uploadForm = this.formBuilder.group({
      profile: ['']
    });
  }

  onFileSelect(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.uploadForm.get('profile').setValue(file);
    }
  }

  onSubmit() {
    const formData = new FormData();
    formData.append('imageFile', this.uploadForm.get('profile').value);

    this.httpClient.post<any>(this.SERVER_URL, formData).subscribe(
      (res) => console.log(res),
      (err) => console.log(err)
    );
  }


}
