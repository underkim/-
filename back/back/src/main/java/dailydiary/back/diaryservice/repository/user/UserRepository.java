package dailydiary.back.diaryservice.repository.user;

import dailydiary.back.diaryservice.domain.user.User;
import org.springframework.data.mongodb.repository.MongoRepository;
import java.util.Optional;

public interface UserRepository extends MongoRepository<User, String> {
    // username으로 사용자를 찾는 메서드
    Optional<User> findByUsername(String username);
}
