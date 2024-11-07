package dailydiary.back.diaryservice.service.user;

import dailydiary.back.diaryservice.domain.user.User;
import dailydiary.back.diaryservice.repository.user.UserRepository;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
@Service
public class UserService {
    private final UserRepository userRepository;

    @Autowired
    public UserService(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    // MongoDB에 사용자 저장
    public void saveUserSession(String accessToken) {
        User user = new User();
        user.setAccessToken(accessToken);
        userRepository.save(user);
    }
}
