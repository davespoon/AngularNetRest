﻿﻿import {Injectable} from '@angular/core';
import {HttpClient, HttpResponse, HttpErrorResponse} from '@angular/common/http';

import {Observable} from 'rxjs';
import {map, catchError} from 'rxjs/operators';

import {IBoardGame, IBoardGameGenre, IFeedback, IPagedResults} from '../../shared/interfaces';
import {environment} from '../../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  url = '/api/messages';
  baseUrl = environment.apiUrl;
  baseBoardGamesUrl = this.baseUrl + 'boardGames';
  baseBoardGameGenresUlr = this.baseUrl + 'boardGameGenres';

  constructor(private http: HttpClient) {
  }

  getBoardGames(): Observable<IBoardGame[]> {
    return this.http.get<IBoardGame[]>(this.baseBoardGamesUrl)
      .pipe(
        map(boardGames => {
          // this.calculateBoardGamesOrderTotal(boardGames);
          return boardGames;
        }),
        catchError(this.handleError)
      );
  }

  getBoardGamesResult(): Observable<IPagedResults<IBoardGame[]>> {
    return this.http.get<IBoardGame[]>(this.baseBoardGamesUrl)
      .pipe(
        map(boardGames => {
          // this.calculateBoardGamesOrderTotal(boardGames);
          return {
            results: boardGames,
            totalRecords: 5
          };
        }),
        catchError(this.handleError)
      );
  }


  getBoardGamesPage(page: number, pageSize: number): Observable<IPagedResults<IBoardGame[]>> {
    return this.http.get<IBoardGame[]>(`${this.baseBoardGamesUrl}/page/${page}/${pageSize}`, {observe: 'response'})
      .pipe(
        map((res) => {
          //Need to observe response in order to get to this header (see {observe: 'response'} above)
          const totalRecords = +res.headers.get('x-inlinecount');
          let boardGames = res.body as IBoardGame[];
          // this.calculateBoardGamesOrderTotal(boardGames);
          return {
            results: boardGames,
            totalRecords: totalRecords
          };
        }),
        catchError(this.handleError)
      );
  }


  // calculateBoardGamesOrderTotal(boardGames: IBoardGame[]) {
  //   for (let boardGame of boardGames) {
  //     if (boardGame && boardGame.orders) {
  //       let total = 0;
  //       for (let order of boardGame.orders) {
  //         total += (order.price * order.quantity);
  //       }
  //       boardGame.orderTotal = total;
  //     }
  //   }
  // }


  getMessage(): Observable<string> {
    return this.http.get<string>(this.url)
      .pipe(
        map(res => res['data']),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    console.error('Server error:', error);
    if (error.error instanceof Error) {
      const errMessage = error.error.message;
      return Observable.throw(errMessage);
    }
    return Observable.throw(error || 'Server error');
  }
}
