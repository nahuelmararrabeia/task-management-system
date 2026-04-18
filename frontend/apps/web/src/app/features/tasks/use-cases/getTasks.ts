import { TaskRepository } from "@/app/features/tasks/repositories/interfaces/TaskRepository";

export class GetTasksUseCase {
  constructor(private repo: TaskRepository) {}

  async execute() {
    return await this.repo.getAll();
  }
}