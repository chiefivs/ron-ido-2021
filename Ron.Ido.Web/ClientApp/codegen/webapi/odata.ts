//  Сгенерировано на основе серверного кода. Не изменять!!!
    //  Ron.Ido.BM.Models.OData.ODataRequest
    export interface IODataRequest {
        skip:number;
        take:number;
        filters:IODataFilter[];
        orders:IODataOrder[];
    }

    //  Ron.Ido.BM.Models.OData.ODataFilter
    export interface IODataFilter {
        field:string;
        aliases:string[];
        type:ODataFilterTypeEnum;
        values:string[];
    }

    //  Ron.Ido.BM.Models.OData.ODataFilterTypeEnum
    export enum ODataFilterTypeEnum {
        Equals = 0,
        NotEquals = 1,
        LessThan = 2,
        GreatThan = 3,
        LessThanOrEqual = 4,
        GreatThanOrEqual = 5,
        In = 6,
        BetweenNone = 7,
        BetweenLeft = 8,
        BetweenRight = 9,
        BetweenAll = 10,
        Starts = 11,
        Contains = 12
    }

    //  Ron.Ido.BM.Models.OData.ODataOrder
    export interface IODataOrder {
        field:string;
        direct:ODataOrderTypeEnum;
    }

    //  Ron.Ido.BM.Models.OData.ODataOrderTypeEnum
    export enum ODataOrderTypeEnum {
        Asc = 0,
        Desc = 1
    }

    //  Ron.Ido.BM.Models.OData.ODataPage<T>
    export interface IODataPage<T> {
        items:T[];
        total:number;
        skip:number;
        size:number;
    }

    //  Ron.Ido.BM.Models.OData.ODataOption
    export interface IODataOption {
        value:any;
        text:string;
        parent:any;
    }

    //  Ron.Ido.BM.Models.OData.ODataForm<TDto>
    export interface IODataForm<TDto> {
        item:TDto;
        options:{[key:string]:IODataOption[]};
    }

    //  Ron.Ido.BM.Models.Storage.FileInfoDto
    export interface IFileInfoDto {
        uid:any;
        name:string;
        size:number;
        contentType:string;
    }

