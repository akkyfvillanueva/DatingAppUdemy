import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { PaginatedResult, Pagination } from '../_models/Pagination';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  members!: Partial<Member[]> | null;
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 5;
  pagination!: Pagination | null;

  constructor(private membersService: MembersService) { 
  }

  ngOnInit(): void {
    this.loadLikes();
  }

  loadLikes() {
    this.membersService.getLikes(this.predicate, this.pageNumber, this.pageSize).subscribe(response => {
      this.members = response.result;
      this.pagination = response.pagination;
      console.log(this.members);
    })
  }

  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadLikes();
  }
}
