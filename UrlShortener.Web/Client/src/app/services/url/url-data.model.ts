export type UrlBaseData = {
    id: number,
    longValue: string,
    shortValue: string,
    isOwner: boolean
};

export type Creator = {
    firstName: string,
    lastName: string,
}

export type UrlDetailedData = UrlBaseData & {
    createdBy: Creator,
    createdDate: Date,
    resourceType: string
}
