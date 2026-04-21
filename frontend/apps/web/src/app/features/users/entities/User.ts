export interface UserProps {
    id: string,
    name: string,
}

export class User {
    private props: UserProps;

    constructor(props: UserProps) {
        this.props = props;
    }

    get id() {
        return this.props.id;
    }

    get name() {
        return this.props.name;
    }
}