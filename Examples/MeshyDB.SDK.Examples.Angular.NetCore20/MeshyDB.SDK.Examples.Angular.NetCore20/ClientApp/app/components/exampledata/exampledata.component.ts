import { Component, Inject } from '@angular/core';
import { Http, RequestOptions } from '@angular/http';
import { fakeAsync } from '@angular/core/testing';

@Component({
    selector: 'exampledata',
    templateUrl: './exampledata.component.html'
})
export class ExampleDataComponent {
    public examples: PageResult<ExampleData> | null = null;

    favoriteNumber: number | null = null;
    name: string = "";

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.getData();
    }

    onFilterClicked = () => {
        this.getData();
    };

    onResetClicked = () => {
        this.favoriteNumber = null;
        this.name = "";
        this.getData();
    };

    editing = false;
    editName = "";
    editNumber: number | null = null;
    id = "";

    onAddNewClicked = () => {
        this.editing = true;
        this.editName = "";
        this.editNumber = null;
        this.id = "";
    };

    onSaveClicked = () => {

        const isNew = this.id === "";

        const data: ExampleData = {
            id: this.id,
            name: this.editName,
            favoriteNumber: this.editNumber || 0
        };

        if (isNew) {
            this.http.post(this.baseUrl + 'api/ExampleData', data).subscribe(result => {
                this.getData();
            }, error => console.error(error));
        } else {
            this.http.put(this.baseUrl + 'api/ExampleData/' + this.id, data).subscribe(result => {
                this.getData();
            }, error => console.error(error));
        }
    };

    onCancelClicked = () => {
        this.editing = false;
    };

    onEditClicked = (data: ExampleData) => {
        this.editing = true;
        this.editNumber = data.favoriteNumber;
        this.editName = data.name;
        this.id = data.id;
    };

    onDeleteClicked = (data: ExampleData) => {
        this.http.delete(this.baseUrl + 'api/ExampleData/' + data.id).subscribe(result => {
            this.getData();
        }, error => console.error(error));
    };

    private getData = () => {
        this.editing = false;
        this.examples = null;

        this.http.get(this.baseUrl + 'api/ExampleData', {
            search: {
                "favoriteNumber": this.favoriteNumber,
                "name": this.name
            }
        }).subscribe(result => {
            this.examples = result.json() as PageResult<ExampleData>;
        }, error => console.error(error));
    };
}

interface PageResult<T> {
    page: number;
    pageSize: number;
    results: T[];
    totalRecords: number;
}

interface ExampleData {
    id: string;
    name: string;
    favoriteNumber: number;
}
