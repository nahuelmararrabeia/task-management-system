import { TaskRepository } from "@/app/features/tasks/repositories/interfaces/TaskRepository";

export class UpdateTaskStatusUseCase {
  constructor(private repo: TaskRepository) {}

  async execute(taskId: string, status: string) {
    await this.repo.updateStatus(taskId, status);
  }
}