import {Component, OnInit} from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';

import {DataService} from '../core/services/data.service';
import {IBoardGame, IBoardGameGenre, IFeedback, IImage} from '../shared/interfaces';

@Component({
  selector: 'boardGame-edit',
  templateUrl: './boardGame-edit.component.html'
})
export class BoardGameEditComponent implements OnInit {

  boardGame: IBoardGame = {
    boardGameGenreId: '', image: undefined, imageId: '',
    boardGameGenre: undefined,
    feedbacks: [], longDescription: '', price: 0,
    title: '',
    shortDescription: ''
  };

  boardGameGenres: IBoardGameGenre[];
  errorMessage: string;
  deleteMessageEnabled: boolean;
  operationText: string = 'Insert';

  constructor(private router: Router,
              private route: ActivatedRoute,
              private dataService: DataService) {
  }

  ngOnInit() {
    let boardGameId = this.route.snapshot.params['id'];
    if (boardGameId !== '0') {
      this.operationText = 'Update';
      this.getBoardGame(boardGameId);
    }

    this.getBoardGameGenres();
  }

  getBoardGame(boardGameId: string) {
    this.dataService.getBoardGame(boardGameId)
      .subscribe((boardGame: IBoardGame) => {
          this.boardGame = boardGame;
        },
        (err: any) => console.log(err));
  }

  getBoardGameGenres() {
    this.dataService.getBoardGameGenres().subscribe((boardGameGenres: IBoardGameGenre[]) => this.boardGameGenres = boardGameGenres);
  }

  submit() {

    if (this.boardGame.boardGameId) {

      this.dataService.updateBoardGame(this.boardGame)
        .subscribe((boardGame: IBoardGame) => {
            if (boardGame) {
              this.router.navigate(['/boardGames']);
            } else {
              this.errorMessage = 'Unable to save boardGame';
            }
          },
          (err: any) => console.log(err));

    } else {

      this.dataService.insertBoardGame(this.boardGame)
        .subscribe((boardGame: IBoardGame) => {
            if (boardGame) {
              this.router.navigate(['/boardGames']);
            } else {
              this.errorMessage = 'Unable to add boardGame';
            }
          },
          (err: any) => console.log(err));

    }
  }

  cancel(event: Event) {
    event.preventDefault();
    this.router.navigate(['/']);
  }

  delete(event: Event) {
    event.preventDefault();
    this.dataService.deleteBoardGame(this.boardGame.boardGameId)
      .subscribe((status: boolean) => {
          if (status) {
            this.router.navigate(['/boardGames']);
          } else {
            this.errorMessage = 'Unable to delete boardGames';
          }
        },
        (err) => console.log(err));
  }

}
