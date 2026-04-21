import { TaskRepository } from "@/app/features/tasks/repositories/interfaces/TaskRepository";

export class GetTaskUseCase {
  constructor(private repo: TaskRepository) {}

  async execute(id: string) {
    return await this.repo.getById(id);
  }
}