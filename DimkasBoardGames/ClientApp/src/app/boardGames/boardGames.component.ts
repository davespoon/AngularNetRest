// import {Component, OnInit} from '@angular/core';
// import {Router} from '@angular/router';
//
// import {DataFilterService} from '../core/data-filter.service';
// import {DataService} from '../core/data.service';
// import {IBoardGame, IOrder, IPagedResults} from '../shared/interfaces';
//
// @Component({
//   selector: 'boardGames',
//   templateUrl: './boardGames.component.html'
// })
// export class BoardGamesComponent implements OnInit {
//
//   title: string;
//   boardGames: IBoardGame[] = [];
//   filteredBoardGames: IBoardGame[] = [];
//
//   totalRecords: number = 0;
//   pageSize: number = 10;
//
//   constructor(private router: Router,
//               private dataService: DataService,
//               private dataFilter: DataFilterService) {
//   }
//
//   ngOnInit() {
//     this.title = 'Board Games';
//     this.getBoardGamesPage(1);
//   }
//
//   filterChanged(filterText: string) {
//     if (filterText && this.customers) {
//       let props = ['firstName', 'lastName', 'address', 'city', 'state.name', 'orderTotal'];
//       this.filteredBoardGames = this.dataFilter.filter(this.boardGames, props, filterText);
//     } else {
//       this.filteredBoardGames = this.boardGames;
//     }
//   }
//
//   pageChanged(page: number) {
//     this.getBoardGamesPage(page);
//   }
//
//   getBoardGamesPage(page: number) {
//     this.dataService.getBoardGamesPage((page - 1) * this.pageSize, this.pageSize)
//       .subscribe((response: IPagedResults<IBoardGame[]>) => {
//           this.boardGames = this.filteredBoardGames = response.results;
//           this.totalRecords = response.totalRecords;
//         },
//         (err: any) => console.log(err),
//         () => console.log('getBoardGamesPage() retrieved board games'));
//   }
//
// }
