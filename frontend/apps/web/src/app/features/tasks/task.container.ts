import { TaskApiRepository } from "./repositories/TaskApiRepository";
import { CreateTaskUseCase } from "./use-cases/createTask";
import { GetTaskUseCase } from "./use-cases/getTask";
import { GetTasksUseCase } from "./use-cases/getTasks";
import { UpdateTaskStatusUseCase } from "./use-cases/updateTaskStatus";

export function createTasksContainer() {
  const taskRepository = new TaskApiRepository();

  return {
    taskRepository,
    getTasks: new GetTasksUseCase(taskRepository),
    getTask: new GetTaskUseCase(taskRepository),
    createTask: new CreateTaskUseCase(taskRepository),
    changeTaskStatus: new UpdateTaskStatusUseCase(taskRepository),
  };
}