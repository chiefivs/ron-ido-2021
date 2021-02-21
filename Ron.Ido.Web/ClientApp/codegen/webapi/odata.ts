//  Сгенерировано на основе серверного кода. Не изменять!!!
    //  Ron.Ido.BM.Commands.Admin.Access.GetUsersPageCommand
    export interface IGetUsersPageCommand {
        skip:number;
        take:number;
        filters:IODataFilter[];
        orders:IODataOrder[];
    }

    //  Ron.Ido.BM.Models.OData.ODataFilter
    export interface IODataFilter {
        field:string;
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
