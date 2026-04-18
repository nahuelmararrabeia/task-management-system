import { TaskRepository } from "@/app/features/tasks/repositories/interfaces/TaskRepository";
import { Task } from "@/app/features/tasks/entities/Task";

export class CreateTaskUseCase {
  constructor(private repo: TaskRepository) {}

  async execute(title: string) {
    const task = new Task({
      id: crypto.randomUUID(),
      title,
      createdAt: new Date(),
    });

    await this.repo.create(task);
  }
}