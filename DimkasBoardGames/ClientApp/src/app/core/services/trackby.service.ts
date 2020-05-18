import {Injectable} from '@angular/core';

import {IBoardGame} from '../../shared/interfaces';

@Injectable({
  providedIn: 'root'
})
export class TrackByService {

  boardGame(index: number, boardGame: IBoardGame) {
    return boardGame.boardGameId;
  }

}
