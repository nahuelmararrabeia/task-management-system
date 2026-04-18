import { TaskRepository } from "@/app/features/tasks/repositories/interfaces/TaskRepository";
import { Task } from "@/app/features/tasks/entities/Task";

export class CreateTaskUseCase {
  constructor(private repo: TaskRepository) {}

  async execute(title: string): Promise<any> {
    const task = new Task({
      title,
      description: "",
    });

    return await this.repo.create(task);
  }
}