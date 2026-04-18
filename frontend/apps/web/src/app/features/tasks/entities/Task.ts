export type TaskStatus = "Pending" | "InProgress" | "Completed";

export interface TaskProps {
  id?: string;
  title: string;
  description: string;
  status?: TaskStatus;
  createdAt?: Date;
}

export class Task {
  private props: TaskProps;

  constructor(props: TaskProps) {
    this.props = props;
  }

  // getters
  get id() {
    return this.props.id;
  }

  get title() {
    return this.props.title;
  }

  get description() {
    return this.props.description;
  }

  get status() {
    return this.props.status;
  }

  get createdAt() {
    return this.props.createdAt;
  }

  changeStatus(newStatus: TaskStatus) {
    if (this.props.status === newStatus) return;

    if (!this.props.title) {
      throw new Error("Task must have a title");
    }

    this.props.status = newStatus;
  }

  rename(title: string) {
    if (!title.trim()) {
      throw new Error("Title cannot be empty");
    }

    this.props.title = title;
  }
}